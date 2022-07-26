<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router'
import { v4 as uuid } from 'uuid'
import Api from '@/services/api'
import type CommonResultDto from '@/dtos/commonResultDto'

export default defineComponent({
    setup() {
        const router = useRouter()
        const isModalActive = ref(false)
        const isConfirmModalActive = ref(false)
        const isErrorModalActive = ref(false)
        const title = ref<string>()
        const description = ref<string>()

        const onSubmit = () => {
            if (title.value == undefined)
                return

            isModalActive.value = true

            let id = uuid()

            Api.createBook(id, title.value, description.value, (result: CommonResultDto<string>) => {
                isModalActive.value = false

                if ((result.errors?.length ?? 0) > 0) {
                    isErrorModalActive.value = true
                    return
                }

                router.push(`/books/${id}`)
            })
        }

        const onCancel = () => {
            isConfirmModalActive.value = true
        }

        const onConfirmClose = () => {
            isConfirmModalActive.value = false
        }

        const onConfirmDiscard = () => {
            router.push('/')
        }

        const onErrorClose = () => {
            isErrorModalActive.value = false
        }

        return {
            isModalActive,
            isConfirmModalActive,
            isErrorModalActive,
            title,
            description,
            onSubmit,
            onCancel,
            onConfirmDiscard,
            onConfirmClose,
            onErrorClose
        }
    }
})
</script>

<template>
    <section class="section is-flex-grow-0 is-flex-shrink-1">
        <h1 class="title">Add Book</h1>
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

    <div id="modal" class="modal" :class="{ 'is-active': isModalActive }">
        <div class="modal-background"></div>
        <div class="modal-content">
        </div>
    </div>

    <div id="confirmModal" class="modal" :class="{ 'is-active': isConfirmModalActive }">
        <div class="modal-background"></div>
        <div class="modal-content">
            <div class="card">
                <div class="card-content">
                    <p class="title">
                    Are you sure?
                    </p>
                    <p class="subtitle">
                    Clicking on "Discard changes" will take you to the main page and the changes you made won't be saved.
                    </p>
                </div>
                <footer class="card-footer">
                    <a @click="onConfirmDiscard" href="#" class="card-footer-item">Discard changes</a>
                    <a @click="onConfirmClose" href="#" class="card-footer-item">Cancel</a>
                </footer>
            </div>
        </div>
        <button @click="onConfirmClose" class="modal-close is-large" aria-label="close"></button>
    </div>

    <div id="errorModal" class="modal" :class="{ 'is-active': isErrorModalActive }">
        <div class="modal-background"></div>
        <div class="modal-content">
            <div class="card">
                <div class="card-content">
                    <p class="title">
                    Error
                    </p>
                    <p class="subtitle">
                    Something happened.
                    </p>
                </div>
                <footer class="card-footer">
                    <a @click="onErrorClose" href="#" class="card-footer-item">OK</a>
                </footer>
            </div>
        </div>
        <button @click="onErrorClose" class="modal-close is-large" aria-label="close"></button>
    </div>
</template>

<style>
</style>