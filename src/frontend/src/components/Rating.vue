<script lang="ts">
import { defineComponent, computed } from 'vue'
import type { PropType } from 'vue'

export default defineComponent({
    emits: [
    ],
    props: {
        value: {
            type: Number as PropType<number>,
            default: 0
        }
    },
    setup(props, context) {
        return {
        }
    }
})
</script>

<template>
    <div class="is-flex is-align-items-center">
        <p v-if="value == 0.0" class="is-size-7 has-text-grey-light">Not rated</p>
        <p v-if="value > 0.0">⭐</p>
        <p v-if="value > 0.2">⭐</p>
        <p v-if="value > 0.4">⭐</p>
        <p v-if="value > 0.6">⭐</p>
        <p v-if="value > 0.8">⭐</p>
    </div>
</template>

<style>
</style>