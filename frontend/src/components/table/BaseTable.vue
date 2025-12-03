<script setup lang="ts">
import type {LoanDto} from '../../model/DTOs';
import {BookCheck, BookPlus, Trash2} from 'lucide-vue-next';

defineProps<{
  headers: { key: string; label: string }[]
  rows: any[]
}>();

const actions = defineEmits<{
  (e: 'toggle-status', loan: LoanDto): void;
  (e: 'delete-loan', loan: LoanDto): void;
}>();
</script>

<template>
  <div class="table-wrapper">
    <div class="rounded-2xl overflow-hidden">
      <table class="w-full table-auto border-separate border-spacing-2">
        <thead class="backdrop-blur-md rounded-3xl shadow-sm">
        <tr class="text-center">
          <th
              v-for="header in headers"
              :key="header.key"
              class="px-4 py-2"
          >
            {{ header.label }}
          </th>
          <th class="px-4 py-2 text-left">
            Действия
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
          <td>
              <button
                  class="action-button mr-2"
                  @click="actions('toggle-status', row)"
              >
                <BookCheck v-if="row.status === 1" class="p-0.5" :size="24"/>
                <BookPlus v-else class="p-0.5" :size="24"/>
              </button>

              <button
                  class="action-button"
                  @click="actions('delete-loan', row)"
              >
                <Trash2 class="p-0.5" :size="24"/>
              </button>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
