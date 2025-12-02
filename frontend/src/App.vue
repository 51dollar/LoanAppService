<script setup lang="ts">
import {ref, onMounted, onUnmounted, computed} from 'vue';
import BaseTable from "./components/table/BaseTable.vue";
import {getLoans, createLoan, updateLoan, deleteLoan} from './api/service/LoanService.ts';
import type {LoanDto} from './model/DTOs/LoanDto.ts';
import type {PageResult} from './model/types/PageResult.ts';
import CreateLoanDialog from './components/dialogs/CreateLoanDialog.vue';
import LoanFilterDialog from './components/dialogs/LoanFilterDialog.vue';
import {mapLoansToDisplay} from './utils/formatters.ts';
import type {LoanCreateDto} from './model/DTOs/LoanCreateDto.ts';
import type {LoanQuery} from './model/types/LoanQuery.ts';
import type {LoanUpdateDto} from './model/DTOs/LoanUpdateDto.ts';
import {Redo, Undo} from 'lucide-vue-next';

const pageNumber = ref(1);
const pageSize = ref(10);
const loans = ref<LoanDto[]>([]);
const isLoading = ref(false);

const headers = [
  {key: 'number', label: 'Номер'},
  {key: 'statusLabel', label: 'Статус'},
  {key: 'amount', label: 'Сумма'},
  {key: 'termValue', label: 'Срок'},
  {key: 'interestValue', label: 'Процент'},
  {key: 'createdAt', label: 'Дата создания'},
  {key: 'actions', label: 'Действия'}
];

let interval: number;

const displayRows = computed(() => mapLoansToDisplay(loans.value));

const loadLoans = async (filters?: LoanQuery, page = 1, size = 10) => {
  isLoading.value = true;

  try {
    await new Promise(resolve => setTimeout(resolve, 500));

    const result: PageResult<LoanDto> = await getLoans(filters, page, size);
    loans.value = result.data;
  } catch (e) {
    console.error('Ошибка загрузки данных', e);
  } finally {
    isLoading.value = false;
  }
};

const goToPage = async (page: number) => {
  if (page < 1) return;
  pageNumber.value = page;
  await loadLoans(undefined, pageNumber.value, pageSize.value);
};

const nextPage = () => goToPage(pageNumber.value + 1);
const prevPage = () => goToPage(pageNumber.value - 1);

const createLoanDialog = ref<InstanceType<typeof CreateLoanDialog> | null>(null);
const filterDialog = ref<InstanceType<typeof LoanFilterDialog> | null>(null);

const openCreateDialog = () => createLoanDialog.value?.open();
const openFilterDialog = () => filterDialog.value?.open();

const onApplyFilters = async (filters: LoanQuery) => {
  pageNumber.value = 1;
  await loadLoans(filters);
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

const updateStatus = async (loan: LoanDto) => {
  const dto: LoanUpdateDto = {status: loan.status === 1 ? 2 : 1};

  try {
    await updateLoan(loan.number, dto);
    await loadLoans();
  } catch (e) {
    console.error('Ошибка смены статуса:', e);
  }
};

const removeLoan = async (loan: LoanDto) => {
  if (!confirm(`Удалить заявку №${loan.number}?`)) return;

  try {
    await deleteLoan(loan.number);
    await loadLoans();
  } catch (e) {
    console.error('Ошибка удаления:', e);
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
      <div class="flex-1"></div>
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
        v-if="!isLoading"
        :headers="headers"
        :rows="displayRows"
        @toggle-status="updateStatus"
        @delete-loan="removeLoan"
    />

    <div v-else class="space-y-2">
      <div v-for="n in pageSize" :key="n" class="h-10 bg-white/5 rounded-4xl animate-pulse"></div>
    </div>

    <div class="flex space-x-3 p-4 justify-center">
      <button @click="prevPage" :disabled="pageNumber === 1" class="default-button">
        <Undo class="p-0.5" :size="20" />
      </button>

      <span>{{ pageNumber }}</span>

      <button @click="nextPage" class="default-button">
        <Redo class="p-0.5" :size="20" />
      </button>
    </div>
  </div>
</template>