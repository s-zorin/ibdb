<script lang="ts">
import { defineComponent, reactive, ref } from 'vue'

export default defineComponent({
    setup() {
        const loc = ref(location.hostname)

        return {
            loc
        }
    }
})
</script>

<template>
<h1>Books</h1>
<p>{{loc}}</p>
</template>