<script lang="ts">
import { defineComponent, computed } from 'vue'
import type { PropType } from 'vue'

export default defineComponent({
    emits: [
        'page'
    ],
    props: {
        pageIndex: {
            type: Number as PropType<number>,
            default: 0
        },
        pageMaxIndex: { 
            type: Number as PropType<number>,
            default: 0
        }
    },
    setup(props, context) {
        const canGoPrevious = computed(() => {
            return props.pageIndex > 0
        })

        const canGoNext = computed(() => {
            return props.pageIndex < props.pageMaxIndex
        })

        const canGoFirst = computed(() => {
            return props.pageIndex > 1
        })

        const canGoLast = computed(() => {
            return props.pageIndex < props.pageMaxIndex - 1
        })

        const condition = <T>(
            pageIndex: number,
            pageMaxIndex: number,
            onLeft: () => T,
            onRight: () => T,
            onMiddle: () => T) : T => {
            if (pageIndex < 2)
                return onLeft()
            else if (pageIndex > pageMaxIndex - 2)
                return onRight()
            else
                return onMiddle()
        }

        const button1PageIndex = computed(() => {
            return condition(
                props.pageIndex,
                props.pageMaxIndex,
                () => 0,
                () => props.pageMaxIndex - 2,
                () => props.pageIndex - 1)
        })

        const button2PageIndex = computed(() => {
            return condition(
                props.pageIndex,
                props.pageMaxIndex,
                () => 1,
                () => props.pageMaxIndex - 1,
                () => props.pageIndex)
        })

        const button3PageIndex = computed(() => {
            return condition(
                props.pageIndex,
                props.pageMaxIndex,
                () => 2,
                () => props.pageMaxIndex,
                () => props.pageIndex + 1)
        })

        const button1IsCurrent = computed(() => {
            return condition(
                props.pageIndex,
                props.pageMaxIndex,
                () => props.pageIndex == 0,
                () => props.pageIndex == props.pageMaxIndex - 2,
                () => false)
        })

        const button2IsCurrent = computed(() => {
            return condition(
                props.pageIndex,
                props.pageMaxIndex,
                () => props.pageIndex == 1,
                () => props.pageIndex == props.pageMaxIndex - 1,
                () => true)
        })

        const button3IsCurrent = computed(() => {
            return condition(
                props.pageIndex,
                props.pageMaxIndex,
                () => props.pageIndex == 2,
                () => props.pageIndex == props.pageMaxIndex,
                () => false)
        })

        const onPrevious = () => {
            if (canGoPrevious.value)
                context.emit('page', props.pageIndex - 1)
        }
        
        const onNext = () => {
            if (canGoNext.value)
                context.emit('page', props.pageIndex + 1)
        }

        const onPage = (x: number) => {
            context.emit('page', x)
        }

        return {
            canGoPrevious,
            canGoNext,
            canGoFirst,
            canGoLast,
            button1PageIndex,
            button2PageIndex,
            button3PageIndex,
            button1IsCurrent,
            button2IsCurrent,
            button3IsCurrent,
            onPrevious,
            onNext,
            onPage
        }
    }
})
</script>

<template>
    <nav class="pagination" role="navigation">
        <a @click="onPrevious" class="pagination-previous" :class="{ 'is-disabled': !canGoPrevious }">Previous</a>
        <a @click="onNext" class="pagination-next" :class="{ 'is-disabled': !canGoNext }">Next page</a>
        <ul class="pagination-list">
            <li @click="onPage(0)" v-if="canGoFirst">
                <a class="pagination-link">1</a>
            </li>
            <li v-if="canGoFirst">
                <span class="pagination-ellipsis">&hellip;</span>
            </li>
            <li>
                <a @click="onPage(button1PageIndex)" class="pagination-link" :class="{ 'is-current': button1IsCurrent }">{{ button1PageIndex + 1 }}</a>
            </li>
            <li v-if="pageMaxIndex > 0">
                <a @click="onPage(button2PageIndex)" class="pagination-link" :class="{ 'is-current': button2IsCurrent }">{{ button2PageIndex + 1 }}</a>
            </li>
            <li v-if="pageMaxIndex > 1">
                <a @click="onPage(button3PageIndex)" class="pagination-link" :class="{ 'is-current': button3IsCurrent }">{{ button3PageIndex + 1 }}</a>
            </li>
            <li v-if="canGoLast">
                <span class="pagination-ellipsis">&hellip;</span>
            </li>
            <li @click="onPage(pageMaxIndex)" v-if="canGoLast">
                <a class="pagination-link">{{ pageMaxIndex + 1 }}</a>
            </li>
        </ul>
    </nav>
</template>

<style>
</style>