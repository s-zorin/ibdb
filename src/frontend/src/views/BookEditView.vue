<script lang="ts">
import { defineComponent, ref, watchEffect } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import Api from '@/services/api'
import type CommonResultDto from '@/dtos/commonResultDto'
import EmptyModal from '@/components/EmptyModal.vue'
import ConfirmModal from '@/components/ConfirmModal.vue'
import MessageModal from '@/components/MessageModal.vue'

export default defineComponent({
    components: {
        EmptyModal,
        ConfirmModal,
        MessageModal
    },
    setup() {
        const router = useRouter()
        const route = useRoute()
        const isEmptyModalActive = ref(false)
        const isConfirmModalActive = ref(false)
        const isErrorModalActive = ref(false)
        const id = route.params.id as string | undefined
        const title = ref<string>()
        const description = ref<string>()

        const onSubmit = () => {
            if (title.value == undefined)
                return

            if (id == undefined)
                return

            isEmptyModalActive.value = true

            Api.updateBook(id, title.value, description.value, (result: CommonResultDto<string>) => {
                isEmptyModalActive.value = false

                if (result.errors.length > 0) {
                    isErrorModalActive.value = true
                    return
                }

                router.push(`/books/${id}`)
            })
        }

        const onCancel = () => {
            isConfirmModalActive.value = true
        }

        const onConfirmCancel = () => {
            isConfirmModalActive.value = false
        }

        const onConfirmConfirm = () => {
            router.push('/')
        }

        const onErrorOk = () => {
            isErrorModalActive.value = false
        }

        watchEffect(async () => {
            if (id == undefined) {
                router.push('/')
                return
            }

            isEmptyModalActive.value = true

            const result = await Api.getBook(id)

            isEmptyModalActive.value = false

            if (result.errors.length > 0) {
                isErrorModalActive.value = true
                return
            }

            title.value = result.value?.title
            description.value = result.value?.description
        })

        return {
            isEmptyModalActive,
            isConfirmModalActive,
            isErrorModalActive,
            title,
            description,
            onSubmit,
            onCancel,
            onConfirmConfirm,
            onConfirmCancel,
            onErrorOk
        }
    }
})
</script>

<template>
    <section class="section is-flex-grow-0 is-flex-shrink-1">
        <h1 class="title">Edit Book</h1>
    </section>

    <section class="section is-flex-grow-0 is-flex-shrink-1">
        <div class="field">
            <label class="label">Title</label>
            <div class="control">
                <input v-model="title" class="input" type="text" placeholder="Enter book's title here">
            </div>
        </div>

        <div class="field">
            <label class="label">Description</label>
            <div class="control">
                <textarea v-model="description" class="textarea" placeholder="Enter book's description here"></textarea>
            </div>
        </div>

        <div class="field is-grouped">
            <div class="control">
                <button @click="onSubmit" class="button is-link">Submit</button>
            </div>
            <div class="control">
                <button @click="onCancel" class="button is-link is-light">Cancel</button>
            </div>
        </div>
    </section>

    <EmptyModal
        :is-active="isEmptyModalActive" />

    <ConfirmModal
        :is-active="isConfirmModalActive"
        title="Are you sure?"
        message="Clicking on &quot;Discard changes&quot; will take you to the main page and the changes you made won't be saved."
        confirm-button-caption="Discard"
        cancel-button-caption="Cancel"
        @confirm="onConfirmConfirm"
        @cancel="onConfirmCancel" />

    <MessageModal
        :is-active="isErrorModalActive"
        title="Error"
        message="Unable to complete operation."
        @ok="onErrorOk" />
</template>

<style>
</style>