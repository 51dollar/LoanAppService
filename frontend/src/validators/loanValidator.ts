import type {LoanCreateDto} from '../api/DTOs/LoanCreateDto.ts';

export interface LoanErrors {
    number: string
    amount: string
    termValue: string
    interestValue: string
}

export function validateLoanCreate(form: LoanCreateDto): LoanErrors {
    return {
        number: form.number.trim() ? '' : 'Номер заявки обязателен',
        amount: form.amount > 0 ? '' : 'Сумма должна быть больше 0',
        termValue: form.termValue > 0 ? '' : 'Срок должен быть больше 0',
        interestValue: form.interestValue > 0 ? '' : 'Процент должен быть больше 0'
    }
}