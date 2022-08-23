<script lang="ts">
import { defineComponent, ref, watchEffect } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { v4 as uuid } from 'uuid'
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
        const title = ref<string>()
        const score = ref<number>(5)
        const text = ref<string>()

        const bookId = route.query.bookid as string | undefined

        const onSubmit = () => {
            if (bookId == undefined)
                return

            isEmptyModalActive.value = true

            const id = uuid()
            const normalizedScore = score.value / 5 

            Api.createReview(id, bookId, text.value, normalizedScore, (result: CommonResultDto<string>) => {
                isEmptyModalActive.value = false

                if (result.errors.length > 0) {
                    isErrorModalActive.value = true
                    return
                }

                router.push(`/reviews/${id}`)
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
            if (bookId == undefined) {
                router.push('/')
                return
            }

            isEmptyModalActive.value = true

            const result = await Api.getBook(bookId)

            isEmptyModalActive.value = false

            if (result.errors.length > 0) {
                isErrorModalActive.value = true
                return
            }

            if (result.value == undefined) {
                router.push('/')
                return
            }

            title.value = result.value.title
        })

        return {
            isEmptyModalActive,
            isConfirmModalActive,
            isErrorModalActive,
            title,
            score,
            text,
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
        <h1 class="title">Add Review</h1>
    </section>

    <section class="section is-flex-grow-0 is-flex-shrink-1">
        <div class="field">
            <label class="label">Book</label>
            <div class="control">
                <input v-model="title" class="input" type="text" disabled>
            </div>
        </div>

        <div class="field">
            <label class="label">Score</label>
            <div class="select">
            <select v-model="score">
                <option>5</option>
                <option>4</option>
                <option>3</option>
                <option>2</option>
                <option>1</option>
            </select>
            </div>
        </div>

        <div class="field">
            <label class="label">Text</label>
            <div class="control">
                <textarea v-model="text" class="textarea" placeholder="Enter your review here"></textarea>
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
        confirm-button-caption="Discard changes"
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