<script setup lang="ts">
import {ref, onMounted, onUnmounted, computed} from 'vue';
import BaseTable from "./components/table/BaseTable.vue";
import {getLoans, createLoan} from './api/service/LoanService.ts';
import type {LoanDto} from './model/DTOs/LoanDto.ts';
import type {PageResult} from './model/types/PageResult.ts';
import CreateLoanDialog from './components/dialogs/CreateLoanDialog.vue';
import LoanFilterDialog from './components/dialogs/LoanFilterDialog.vue';
import {mapLoansToDisplay} from './utils/formatters.ts';
import type {LoanCreateDto} from './model/DTOs/LoanCreateDto.ts';
import type {LoanQuery} from './model/types/LoanQuery.ts';

const loans = ref<LoanDto[]>([]);
const totalCount = ref(0);

const headers = [
  {key: 'number', label: 'Номер'},
  {key: 'statusLabel', label: 'Статус'},
  {key: 'amount', label: 'Сумма'},
  {key: 'termValue', label: 'Срок'},
  {key: 'interestValue', label: 'Процент'},
  {key: 'createdAt', label: 'Дата создания'}
];

let interval: number;

const displayRows = computed(() => mapLoansToDisplay(loans.value));

const loadLoans = async (filters?: LoanQuery, pageNumber = 1, pageSize = 10) => {
  const result: PageResult<LoanDto> = await getLoans(filters, pageNumber, pageSize);
  loans.value = result.data;
  totalCount.value = result.totalCount;
};

const createLoanDialog = ref<InstanceType<typeof CreateLoanDialog> | null>(null);
const filterDialog = ref<InstanceType<typeof LoanFilterDialog> | null>(null);

const openCreateDialog = () => {
  createLoanDialog.value?.open();
};
const openFilterDialog = () => {
  filterDialog.value?.open();
};

const onApplyFilters = async (filters: LoanQuery) => {
  await loadLoans(filters, 1, 10);
};
const handleCreateLoan = async (data: LoanCreateDto | null) => {
  if (!data) return;

  try {
    await createLoan(data);
    await loadLoans();
  } catch (e) {
    console.error('Ошибка создания:', e);
  }
};

onMounted(async () => {
  await loadLoans();
});

onUnmounted(() => {
  clearInterval(interval);
});
</script>

<template>
  <div class="main-container">
    <div class="pb-6 flex items-center justify-between">
      <h1 class="text-3xl">Данные с API</h1>

      <div class="flex space-x-2">
        <button
            @click="openFilterDialog"
            class="default-button">
          Получить отфильтрованные данные
        </button>
        <button
            @click="openCreateDialog"
            class="create-button">
          Создать заявку на займ
        </button>
      </div>
      <LoanFilterDialog
          ref="filterDialog"
          @apply="onApplyFilters"
      />

      <CreateLoanDialog
          ref="createLoanDialog"
          @result="handleCreateLoan"
      />
    </div>

    <BaseTable
        :headers="headers"
        :rows="displayRows"
    />

    <p class="mt-6">Всего записей: {{ totalCount }}</p>
  </div>
</template>