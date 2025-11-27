<script setup lang="ts">
import {ref, onMounted, onUnmounted} from 'vue';
import BaseTable from "./components/table/BaseTable.vue";
import {getLoans} from './api/service/LoanService.ts';
import type {LoanDto} from './api/DTOs/LoanDto.ts';
import type {PageResult} from './api/types/PageResult.ts';

const loans = ref<LoanDto[]>([]);
const totalCount = ref(0)

const headers = [
  {key: 'number', label: 'Номер'},
  {key: 'status', label: 'Статус'},
  {key: 'amount', label: 'Сумма'},
  {key: 'termValue', label: 'Срок'},
  {key: 'interestValue', label: 'Процент'},
  {key: 'createdAt', label: 'Дата создания'}
];

let interval: number;

const loadLoans = async () => {
  const result: PageResult<LoanDto> = await getLoans()
  loans.value = result.data
  totalCount.value = result.totalCount
};

onMounted(async () => {
  await loadLoans();
});

onUnmounted(() => {
  clearInterval(interval)
})
</script>

<template>
  <div class="main-container">
    <div class="pb-6 flex items-center justify-between">
      <h1 class="text-3xl">Данные с API</h1>
      <button class="my-button">Создать заявку на займ</button>
    </div>
    <BaseTable
        :headers="headers"
        :rows=loans
    />

    <p class="mt-6">Всего записей: {{ totalCount }}</p>
  </div>
</template>