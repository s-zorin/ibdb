<script lang="ts">
import { defineComponent } from 'vue'
import type { PropType } from 'vue'

export default defineComponent({
    props: {
        isActive: {
            type: Boolean as PropType<boolean>,
            default: false
        },
    },
    setup(props, context) {
        return {
        }
    }
})
</script>

<template>
    <div class="modal" :class="{ 'is-active': $props.isActive }">
        <div class="modal-background"></div>
        <div class="modal-content"></div>
    </div>
</template>

<style>
</style>