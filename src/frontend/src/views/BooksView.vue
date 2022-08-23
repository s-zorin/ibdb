<script lang="ts">
import { defineComponent, ref, watchEffect } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Pagination from '@/components/Pagination.vue'
import Rating from '@/components/Rating.vue'
import type BookDto from '@/dtos/bookDto'
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
        const books = ref<BookDto[]>()
        const book = ref<BookDto>()
        const isModalActive = ref(false)
        const isBookModalActive = ref(false)
        const route = useRoute()
        const router = useRouter()

        const onPage = (x: number) => {
            pageIndex.value = x;
        }

        const onView = async (id: string | undefined) => {
            if (id == undefined)
                return

            isModalActive.value = true
            const result = await Api.getBook(id)
            book.value = result.value
            isModalActive.value = false
            isBookModalActive.value = true
        }

        const onEdit = (id: string | undefined) => {
            router.push(`/books/edit/${id}`)
        }

        const onDelete = (id: string | undefined) => {
            console.log(id)
        }

        const onReview = (id: string | undefined) => {
            router.push(`/reviews/add?bookid=${id}`)
        }

        const onBookModalClose = () => {
            if (route.params.id != undefined)
                router.push('/')

            isBookModalActive.value = false
        }

        watchEffect(async () => {
            const result = await Api.getBooks(pageIndex.value * pageSize, pageSize)

            if (result.errors?.length == 0)
            {
                books.value = result.value?.items
                pageMaxIndex.value = Math.ceil((result.value?.totalCount ?? 0) / pageSize) - 1
            }

            if (route.params.id != undefined) {
                onView(route.params.id as string)
            }
        })

        return {
            book,
            books,
            pageIndex,
            pageMaxIndex,
            isModalActive,
            isBookModalActive,
            onPage,
            onView,
            onEdit,
            onDelete,
            onReview,
            onBookModalClose
        }
    }
})
</script>

<template>
    <section class="section is-flex-grow-0 is-flex-shrink-1">
        <h1 class="title">Books</h1>
    </section>

    <section class="section pt-0 pb-0 is-flex-grow-0 is-flex-shrink-1">
        <Pagination @page="onPage" :page-index="pageIndex" :page-max-index="pageMaxIndex" />
    </section>

    <section id="content" class="section is-flex-grow-1 is-flex-shrink-1">
        <ul>
            <li v-for="book in books" class="item" @click="onView(book.id)">
                <div class="box is-clickable">
                    <div class="is-flex">
                        <p><strong>{{ book.title }}</strong></p>
                        <Rating class="rating" :value="book.rating" />
                    </div>
                    <p>{{ book.description }}</p>
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

    <div id="bookModal" class="modal" :class="{ 'is-active': isBookModalActive }">
        <div class="modal-background"></div>
        <div class="modal-card">
            <header class="modal-card-head">
            <p class="modal-card-title">{{book?.title}}</p>
            <button @click="onBookModalClose" class="delete" aria-label="close"></button>
            </header>
            <section class="modal-card-body">
            {{book?.description}}
            </section>
            <footer class="modal-card-foot">
            <button @click="onEdit(book?.id)" class="button">Edit</button>
            <button @click="onReview(book?.id)" class="button">Review</button>
            <button @click="onDelete(book?.id)" class="button is-danger">Delete</button>
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