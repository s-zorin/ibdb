import { createRouter, createWebHistory } from 'vue-router'
import BooksView from '../views/BooksView.vue'
import BookAddView from '../views/BookAddView.vue'
import ReviewsView from '../views/ReviewsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: BooksView
    },
    {
      path: '/books',
      name: 'books-view',
      component: BooksView
    },
    {
      path: '/books/:id',
      name: 'books-view-single',
      component: BooksView
    },
    {
      path: '/books/add',
      name: 'books-add',
      component: BookAddView
    },
    {
      path: '/reviews',
      name: 'reviews-view',
      component: ReviewsView
    }
  ]
})

export default router
