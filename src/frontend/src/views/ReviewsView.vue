<script lang="ts">
import { defineComponent, ref, watchEffect } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Pagination from '@/components/Pagination.vue'
import Rating from '@/components/Rating.vue'
import EmptyModal from '@/components/EmptyModal.vue'
import ConfirmModal from '@/components/ConfirmModal.vue'
import MessageModal from '@/components/MessageModal.vue'
import type ReviewDto from '@/dtos/reviewDto'
import Api from '@/services/api'

export default defineComponent({
    components: {
        Pagination,
        Rating,
        MessageModal,
        EmptyModal,
        ConfirmModal
    },
    setup() {
        const pageSize = 10
        const pageIndex = ref(0)
        const pageMaxIndex = ref(0)
        const reviews = ref<ReviewDto[]>()
        const review = ref<ReviewDto>()
        const isConfirmModalActive = ref(false)
        const isEmptyModalActive = ref(false)
        const isReviewModalActive = ref(false)
        const isErrorModalActive = ref(false)
        const route = useRoute()
        const router = useRouter()

        const onPage = (x: number) => {
            pageIndex.value = x;
        }

        const onView = async (id: string | undefined) => {
            if (id == undefined)
                return

            isEmptyModalActive.value = true
            const result = await Api.getReview(id)
            review.value = result.value
            isEmptyModalActive.value = false
            isReviewModalActive.value = true
        }

        const onEdit = (id: string | undefined) => {
            router.push(`/reviews/edit/${id}`)
        }

        const onDelete = (id: string | undefined) => {
            isConfirmModalActive.value = true
        }

        const onReviewModalClose = () => {
            if (route.params.id != undefined)
                router.push('/reviews')

            isReviewModalActive.value = false
        }

        const onErrorOk = () => {
            isErrorModalActive.value = false
        }

        const onConfirmCancel = () => {
            isConfirmModalActive.value = false
        }

        const onConfirmConfirm = () => {
            isConfirmModalActive.value = false

            if (review.value?.id == undefined)
                return

            isEmptyModalActive.value = true

            Api.deleteReview(review.value?.id, (result) => {
                isEmptyModalActive.value = false

                if (result.errors.length > 0) {
                    isErrorModalActive.value = true
                    return
                }

                location.reload()
            })
        }

        watchEffect(async () => {
            const result = await Api.getReviews(pageIndex.value * pageSize, pageSize)

            if (result.errors.length == 0)
            {
                reviews.value = result.value?.items
                pageMaxIndex.value = Math.ceil((result.value?.totalCount ?? 0) / pageSize) - 1
            }

            if (route.params.id != undefined) {
                onView(route.params.id as string)
            }
        })

        return {
            review,
            reviews,
            pageIndex,
            pageMaxIndex,
            isEmptyModalActive,
            isReviewModalActive,
            isErrorModalActive,
            isConfirmModalActive,
            onPage,
            onView,
            onEdit,
            onDelete,
            onReviewModalClose,
            onConfirmConfirm,
            onConfirmCancel,
            onErrorOk
        }
    }
})
</script>

<template>
    <section class="section is-flex-grow-0 is-flex-shrink-1">
        <h1 class="title">Reviews</h1>
    </section>

    <section class="section pt-0 pb-0 is-flex-grow-0 is-flex-shrink-1">
        <Pagination @page="onPage" :page-index="pageIndex" :page-max-index="pageMaxIndex" />
    </section>

    <section id="content" class="section is-flex-grow-1 is-flex-shrink-1">
        <ul>
            <li v-for="review in reviews" class="item" @click="onView(review.id)">
                <div class="box is-clickable">
                    <div class="is-flex">
                        <p><strong>{{ review.bookTitle }}</strong></p>
                        <Rating class="rating" :value="review.score" />
                    </div>
                    <p>{{ review.text }}</p>
                </div>
            </li>
        </ul>
    </section>

    <section class="section pt-0 is-flex-grow-0 is-flex-shrink-1">
        <Pagination @page="onPage" :page-index="pageIndex" :page-max-index="pageMaxIndex" />
    </section>

    <div id="reviewModal" class="modal" :class="{ 'is-active': isReviewModalActive }">
        <div class="modal-background"></div>
        <div class="modal-card">
            <header class="modal-card-head">
            <p class="modal-card-title">{{review?.bookTitle}}</p>
            <button @click="onReviewModalClose" class="delete" aria-label="close"></button>
            </header>
            <section class="modal-card-body">
            {{review?.text}}
            </section>
            <footer class="modal-card-foot">
            <button @click="onEdit(review?.id)" class="button">Edit</button>
            <button @click="onDelete(review?.id)" class="button is-danger">Delete</button>
            </footer>
        </div>
    </div>

    <EmptyModal
        :is-active="isEmptyModalActive" />

    <ConfirmModal
        :is-active="isConfirmModalActive"
        title="Are you sure?"
        message="Clicking on &quot;Delete review&quot; will delete the review."
        confirm-button-caption="Delete review"
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
li.item:not(:last-child) {
    margin-bottom: 1rem;
}
.rating {
    padding-left: 1rem;
}
</style>