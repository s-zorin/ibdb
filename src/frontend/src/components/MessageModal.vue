<script lang="ts">
import { defineComponent, computed } from 'vue'
import type { PropType } from 'vue'

export default defineComponent({
    emits: [
        'ok'
    ],
    props: {
        isActive: {
            type: Boolean as PropType<boolean>,
            default: false
        },
        title: {
            type: String as PropType<string>,
            default: 'Message'
        },
        message: { 
            type: String as PropType<string>,
            default: 'Something has happened.'
        },
        okButtonCaption: { 
            type: String as PropType<string>,
            default: 'OK'
        }
    },
    setup(props, context) {
        const onOk = () => {
            context.emit('ok')
        }

        return {
            onOk
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
                    <a @click="onOk" href="#" class="card-footer-item">{{ $props.okButtonCaption }}</a>
                </footer>
            </div>
        </div>
        <button @click="onOk" class="modal-close is-large" aria-label="close"></button>
    </div>
</template>

<style>
</style>