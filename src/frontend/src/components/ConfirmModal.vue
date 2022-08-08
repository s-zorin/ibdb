<script lang="ts">
import { defineComponent, computed } from 'vue'
import type { PropType } from 'vue'

export default defineComponent({
    emits: [
        'confirm',
        'cancel'
    ],
    props: {
        isActive: {
            type: Boolean as PropType<boolean>,
            default: false
        },
        title: {
            type: String as PropType<string>,
            default: 'Confirm'
        },
        message: { 
            type: String as PropType<string>,
            default: 'Are you sure?'
        },
        confirmButtonCaption: { 
            type: String as PropType<string>,
            default: 'OK'
        },
        cancelButtonCaption: { 
            type: String as PropType<string>,
            default: 'Cancel'
        }
    },
    setup(props, context) {
        const onConfirm = () => {
            context.emit('confirm')
        }

        const onCancel = () => {
            context.emit('cancel')
        }

        return {
            onConfirm,
            onCancel
        }
    }
})
</script>

<template>
    <div class="modal" :class="{ 'is-active': $props.isActive }">
        <div class="modal-background"></div>
        <div class="modal-content">
            <div class="card">
                <div class="card-content">
                    <p class="title">
                    {{ $props.title }}
                    </p>
                    <p class="subtitle">
                    {{ $props.message }}
                    </p>
                </div>
                <footer class="card-footer">
                    <a @click="onConfirm" href="#" class="card-footer-item">{{ $props.confirmButtonCaption }}</a>
                    <a @click="onCancel" href="#" class="card-footer-item">{{ $props.cancelButtonCaption }}</a>
                </footer>
            </div>
        </div>
        <button @click="onCancel" class="modal-close is-large" aria-label="close"></button>
    </div>
</template>

<style>
</style>