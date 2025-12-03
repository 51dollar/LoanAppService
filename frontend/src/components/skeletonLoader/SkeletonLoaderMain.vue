<script setup lang="ts">
import { defineProps } from 'vue';

defineProps<{
  rows: number;
}>()

</script>

<template>
  <div class="space-y-2">
    <div
        v-for="n in rows"
        :key="n"
        class="h-10 bg-white/5 rounded-4xl animate-pulse"
    ></div>
  </div>
</template>