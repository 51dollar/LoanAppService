<script setup lang="ts">
import type {LoanDto} from '../../api/DTOs/LoanDto.ts';

defineProps<{
  headers: { key: keyof LoanDto; label: string }[]
  rows: T[]
}>();
</script>

<template>
  <div class="table-wrapper">
    <div class="rounded-2xl overflow-hidden">
      <table class="w-full table-auto border-separate border-spacing-2">
          <thead class="backdrop-blur-md rounded-3xl shadow-sm">
          <tr>
            <th
                v-for="header in headers"
                :key="header.key"
                class="px-4 py-2"
            >
              {{ header.label }}
            </th>
          </tr>
          </thead>

        <tbody class="backdrop-blur-md rounded-3xl shadow-sm">
        <tr v-for="row in rows" :key="row.number">
          <td
              v-for="header in headers"
              :key="header.key"
              class="px-4 py-2 text-center"
          >
            {{ row[header.key] }}
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
