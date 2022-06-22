<script lang="ts">
import { defineComponent, onMounted, reactive, ref } from 'vue'

export default defineComponent({
    props:{
        x: String
    },
    setup(props) {
        const message = ref(0)
        const text = ref('')
        const items = ref(new Array<string>)
        const inp = ref<HTMLParagraphElement>()

        const changeMessage = () => {
            message.value++
            items.value.push(message.value.toString())
        }

        const click2 = () => {
            if (inp.value == null || props.x == null)
                return

            inp.value.textContent = props.x
        }

        onMounted(() => {
            if (inp.value != null)
            {
                inp.value.textContent = 'CONTENT'
            }
        });

        return {
            changeMessage,
            click2,
            message,
            text,
            items,
            inp
        }
    }
})
</script>

<template>
    <h1>CHANGE678789</h1>
    <h1>{{ message }} {{ text }}</h1>
    <input v-model="text"><br/>
    <button @click="changeMessage">CLICK</button>
    <button @click="click2">CLICK2</button>
    <p v-if="text.length > 0">{{ text }}</p>
    <p v-if="text.length > 1">???</p>
    <p v-else>Empty text!</p>
    <ul>
        <li v-for="item in items" @click="click2">Item's text: {{ item }}</li>
    </ul>
    <p ref="inp" ></p>
</template>