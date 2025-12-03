<script setup lang="ts">
import { onMounted } from 'vue';
import BaseTable from './components/table/BaseTable.vue';
import CreateLoanDialog from './components/dialogs/CreateLoanDialog.vue';
import LoanFilterDialog from './components/dialogs/LoanFilterDialog.vue';
import SkeletonLoaderMain from './components/skeletonLoader/SkeletonLoaderMain.vue';
import { Redo, Undo } from 'lucide-vue-next';

import {useLoans} from './composables/useLoans.ts';
import {useDialogs} from './composables/useDialogs.ts';
import {usePagination} from './composables/usePagination.ts';

const {
  displayRows,
  isLoading,
  activeFilters,
  loadLoans,
  createNewLoan,
  updateLoanStatus,
  removeLoan
} = useLoans();

const { dialogRef: createLoanDialog, openDialog: openCreateDialog } = useDialogs<typeof CreateLoanDialog>();
const { dialogRef: filterDialog, openDialog: openFilterDialog } = useDialogs<typeof LoanFilterDialog>();

const headers = [
  { key: 'number', label: 'Номер' },
  { key: 'statusLabel', label: 'Статус' },
  { key: 'amount', label: 'Сумма' },
  { key: 'termValue', label: 'Срок' },
  { key: 'interestValue', label: 'Процент' },
  { key: 'createdAt', label: 'Дата создания' },
];

const {pageNumber, pageSize, nextPage, prevPage} = usePagination();

const handleNext = () =>
    nextPage((page) => loadLoans(activeFilters.value, page, pageSize.value));

const handlePrev = () =>
    prevPage((page) => loadLoans(activeFilters.value, page, pageSize.value));

const onApplyFilters = async (filters: any) => {
  await loadLoans(filters, pageNumber.value, pageSize.value);
};

const handleCreateLoan = async (data: any) => {
  if (!data) return;
  await createNewLoan(data, pageNumber.value, pageSize.value);
};

const wrappedUpdateStatus = (loan: any) =>
    updateLoanStatus(loan, pageNumber.value, pageSize.value);

const wrappedRemoveLoan = (loan: any) =>
    removeLoan(loan, pageNumber.value, pageSize.value);

onMounted(() => loadLoans());
</script>

<template>
  <div class="main-container">
    <div class="pb-6 flex items-center justify-between">
      <div class="flex-1"></div>
      <div class="flex space-x-2">
        <button @click="openFilterDialog" class="default-button">Получить отфильтрованные данные</button>
        <button @click="openCreateDialog" class="create-button">Создать заявку на займ</button>
      </div>

      <LoanFilterDialog ref="filterDialog" @apply="onApplyFilters" />
      <CreateLoanDialog ref="createLoanDialog" @result="handleCreateLoan" />
    </div>

    <BaseTable
        v-if="!isLoading"
        :headers="headers"
        :rows="displayRows"
        @toggle-status="wrappedUpdateStatus"
        @delete-loan="wrappedRemoveLoan"
    />
    <SkeletonLoaderMain :rows="pageSize" v-else/>

    <div class="flex space-x-3 p-4 justify-center">
      <button @click="handlePrev" :disabled="pageNumber === 1" class="default-button">
        <Undo class="p-0.5" :size="20" />
      </button>
      <span>{{ pageNumber }}</span>
      <button @click="handleNext" class="default-button">
        <Redo class="p-0.5" :size="20" />
      </button>
    </div>
  </div>
</template>
