import { createRouter, createWebHistory } from 'vue-router'
import BooksView from '../views/BooksView.vue'
import BookAddView from '../views/BookAddView.vue'
import BookEditView from '../views/BookEditView.vue'
import ReviewsView from '../views/ReviewsView.vue'
import ReviewAddView from '../views/ReviewAddView.vue'
import ReviewEditView from '../views/ReviewEditView.vue'

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
      path: '/books/edit/:id',
      name: 'books-edit',
      component: BookEditView
    },
    {
      path: '/reviews',
      name: 'reviews-view',
      component: ReviewsView
    },
    {
      path: '/reviews/:id',
      name: 'reviews-view-single',
      component: ReviewsView
    },
    {
      path: '/reviews/add',
      name: 'reviews-add',
      component: ReviewAddView
    },
    {
      path: '/reviews/edit/:id',
      name: 'reviews-edit',
      component: ReviewEditView
    }
  ]
})

export default router
