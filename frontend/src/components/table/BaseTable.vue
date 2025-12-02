<script setup lang="ts">
import type {LoanDto} from '../../model/DTOs/LoanDto.ts';
import {BookCheck, BookPlus, Trash2} from 'lucide-vue-next';

type TableRow = Record<string, any> & {
  number: string | number;
  statusLabel?: string;
};

defineProps<{
  headers: { key: keyof LoanDto | 'actions'; label: string }[]
  rows: TableRow[]
}>();

const actions = defineEmits<{
  (e: 'toggle-status', loan: TableRow): void;
  (e: 'delete-loan', loan: TableRow): void;
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
            <template v-if="header.key !== 'actions'">
              {{ row[header.key] }}
            </template>

            <template v-else>
                <button
                    class="action-button mr-2"
                    @click="actions('toggle-status', row)"
                >
                  <BookCheck v-if="row.statusLabel === 'Опубликовано'" class="p-0.5" :size="24"/>
                  <BookPlus v-else class="p-0.5" :size="24"/>
                </button>

                <button
                    class="action-button"
                    @click="actions('delete-loan', row)"
                >
                  <Trash2 class="p-0.5" :size="24"/>
                </button>
            </template>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
