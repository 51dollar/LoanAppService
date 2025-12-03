import { ref, computed } from 'vue';
import type {LoanDto, LoanCreateDto} from '../model/DTOs';
import type {LoanQuery, PageResult} from '../model/types';
import {StatusType} from '../model/types';
import { getLoans, createLoan, updateLoan, deleteLoan } from '../api/service/LoanService.ts';
import { mapLoansToDisplay } from '../utils/formatters';
import { ElMessage } from 'element-plus';

export function useLoans() {
    const loans = ref<LoanDto[]>([]);
    const isLoading = ref(false);
    const activeFilters = ref<LoanQuery | undefined>(undefined);

    const displayRows = computed(() => mapLoansToDisplay(loans.value));

    const loadLoans = async (filters?: LoanQuery, page?: number, size?: number, delay = 300) => {
        isLoading.value = true;
        try {
            if (delay) await new Promise(res => setTimeout(res, delay));
            activeFilters.value = filters ?? activeFilters.value;
            const result: PageResult<LoanDto> = await getLoans(activeFilters.value, page, size);
            loans.value = result.data;
        } catch {
            ElMessage.error('Ошибка загрузки данных');
        } finally {
            isLoading.value = false;
        }
    };

    const createNewLoan = async (data: LoanCreateDto, page: number, size: number) => {
        try {
            await createLoan(data);
            await loadLoans(activeFilters.value, page, size);
        } catch {
            ElMessage.error('Ошибка создания');
        }
    };

    const updateLoanStatus = async (loan: LoanDto, page: number, size: number) => {
        const newStatus = loan.status === StatusType.Published
            ? StatusType.Unpublished
            : StatusType.Published;

        try {
            await updateLoan(loan.number, { status: newStatus });
            loan.status = newStatus;
            await loadLoans(activeFilters.value, page, size, 0);
        } catch {
            ElMessage.error('Ошибка смены статуса');
        }
    };

    const removeLoan = async (loan: LoanDto, page: number, size: number) => {
        if (!confirm(`Удалить заявку №${loan.number}?`)) return;
        try {
            await deleteLoan(loan.number);
            await loadLoans(activeFilters.value, page, size, 0);
        } catch {
            ElMessage.error('Ошибка удаления');
        }
    };

    return {
        loans,
        displayRows,
        isLoading,
        activeFilters,
        loadLoans,
        createNewLoan,
        updateLoanStatus,
        removeLoan
    };
}
