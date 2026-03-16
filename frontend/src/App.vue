<template>
  <main>
    <h1>To-Do List</h1>

    <form @submit.prevent="addTodo">
      <input v-model="newTitle" placeholder="New task..." required />
      <button type="submit">Add</button>
    </form>

    <ul>
      <li v-for="todo in todos" :key="todo.id">
        <span :class="{ done: todo.isCompleted }" @click="toggle(todo.id)">
          {{ todo.title }}
        </span>
        <button @click="remove(todo.id)">✕</button>
      </li>
    </ul>
  </main>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'

interface TodoItem {
  id: number
  title: string
  isCompleted: boolean
}

const API = '/api/todo'

const todos = ref<TodoItem[]>([])
const newTitle = ref('')

async function fetchTodos() {
  const res = await fetch(API)
  todos.value = await res.json()
}

async function addTodo() {
  if (!newTitle.value.trim()) return
  await fetch(API, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(newTitle.value),
  })
  newTitle.value = ''
  await fetchTodos()
}

async function toggle(id: number) {
  await fetch(`${API}/${id}/toggle`, { method: 'PATCH' })
  await fetchTodos()
}

async function remove(id: number) {
  await fetch(`${API}/${id}`, { method: 'DELETE' })
  await fetchTodos()
}

onMounted(fetchTodos)
</script>

<style scoped>
main {
  max-width: 480px;
  margin: 60px auto;
  font-family: sans-serif;
}

h1 {
  margin-bottom: 1rem;
}

form {
  display: flex;
  gap: 8px;
  margin-bottom: 1.5rem;
}

input {
  flex: 1;
  padding: 8px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
}

button {
  padding: 8px 12px;
  cursor: pointer;
}

ul {
  list-style: none;
  padding: 0;
}

li {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 8px 0;
  border-bottom: 1px solid #eee;
}

span {
  cursor: pointer;
}

span.done {
  text-decoration: line-through;
  color: #aaa;
}
</style>
