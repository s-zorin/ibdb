<script lang="ts">
import { defineComponent, ref, watchEffect } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Pagination from '@/components/Pagination.vue'
import Rating from '@/components/Rating.vue'
import type ReviewDto from '@/dtos/reviewDto'
import Api from '@/services/api'

export default defineComponent({
    components: {
        Pagination,
        Rating
    },
    setup() {
        const pageSize = 10
        const pageIndex = ref(0)
        const pageMaxIndex = ref(0)
        const reviews = ref<ReviewDto[]>()
        const review = ref<ReviewDto>()
        const isModalActive = ref(false)
        const isReviewModalActive = ref(false)
        const route = useRoute()
        const router = useRouter()

        const onPage = (x: number) => {
            pageIndex.value = x;
        }

        const onView = async (id: string | undefined) => {
            if (id == undefined)
                return

            isModalActive.value = true
            const result = await Api.getReview(id)
            review.value = result.value
            isModalActive.value = false
            isReviewModalActive.value = true
        }

        const onEdit = (id: string | undefined) => {
            router.push(`/reviews/edit/${id}`)
        }

        const onDelete = (id: string | undefined) => {
            console.log(id)
        }

        const onReviewModalClose = () => {
            if (route.params.id != undefined)
                router.push('/reviews')

            isReviewModalActive.value = false
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
            isModalActive,
            isReviewModalActive,
            onPage,
            onView,
            onEdit,
            onDelete,
            onReviewModalClose
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

    <div id="modal" class="modal" :class="{ 'is-active': isModalActive }">
        <div class="modal-background"></div>
        <div class="modal-content">
        </div>
    </div>

    <div id="bookModal" class="modal" :class="{ 'is-active': isReviewModalActive }">
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
</template>

<style>
li.item:not(:last-child) {
    margin-bottom: 1rem;
}
.rating {
    padding-left: 1rem;
}
</style>