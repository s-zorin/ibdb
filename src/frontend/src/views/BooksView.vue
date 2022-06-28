<script lang="ts">
import { defineComponent, ref, watchEffect, watch } from 'vue'
import Pagination from '@/components/Pagination.vue'
import type BookDto from '@/dtos/bookDto'
import Api from '@/services/api'

export default defineComponent({
    components: {
        Pagination
    },
    setup() {
        const pageSize = 10
        const pageIndex = ref(0)
        const books = ref<BookDto[]>()

        const onPage = (x: number) => {
            pageIndex.value = x;
        }

        watchEffect(async () => {
            books.value = (await Api.getBooks(pageIndex.value * pageSize, pageSize)).value
        })

        return {
            books,
            pageIndex,
            onPage
        }
    }
})
</script>

<template>
    <section class="section is-flex-grow-0 is-flex-shrink-1">
        <h1 class="title">Books</h1>
    </section>

    <section id="content" class="section pt-2 is-flex-grow-1 is-flex-shrink-1">
        <ul>
            <li v-for="book in books" class="item">
                <div class="box">
                    <p>
                        <strong>{{ book.title }}</strong>
                        <br>
                        {{ book.description }}
                        <nav class="level">
                            <div class="level-left">
                            </div>
                            <div class="level-right">
                                <a class="level-item">Edit</a>
                                <a class="level-item">Delete</a>
                                <a class="level-item">Review</a>
                            </div>
                        </nav>
                    </p>
                </div>
            </li>
        </ul>
    </section>

    <section class="section is-flex-grow-0 is-flex-shrink-1">
        <Pagination @page="onPage" :page-index="pageIndex" :page-max-index="9" />
    </section>
</template>

<style>
#content {
    overflow-y: auto;
}

li.item:not(:last-child) {
    margin-bottom: 1rem;
}
</style>