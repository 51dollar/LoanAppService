import type {LoanDto} from '../api/DTOs/LoanDto.ts';
import {StatusType} from '../api/DTOs/StatusType.ts';

function pad2(n: number) {
    return n < 10 ? `0${n}` : `${n}`;
}

export function formatAmountWithCurrency(amount: number | null | undefined, currency = 'BYN'): string {
    if (amount == null || Number.isNaN(amount)) return ''
    const nf = new Intl.NumberFormat('en-US', { maximumFractionDigits: 0 })
    return `${nf.format(Math.round(amount))} ${currency}`
}

export function formatTermDays(term: number | null | undefined): string {
    if (term == null || Number.isNaN(term)) return ''
    return `${term} д.`
}

export function formatInterestPercent(interest: number | null | undefined): string {
    if (interest == null || Number.isNaN(interest)) return ''
    const rounded = Math.round(interest * 100) / 100
    return Number.isInteger(rounded) ? `${rounded}%` : `${rounded.toFixed(2)}%`
}

export function formatDateTimeIsoToDDMMYYYY_HHMM(iso: string | null | undefined): string {
    if (!iso) return ''
    const d = new Date(iso)
    if (Number.isNaN(d.getTime())) return ''
    const day = pad2(d.getDate())
    const month = pad2(d.getMonth() + 1)
    const year = d.getFullYear()
    const hours = pad2(d.getHours())
    const minutes = pad2(d.getMinutes())
    return `${day}.${month}.${year} ${hours}:${minutes}`
}

export function mapLoanToDisplay(l: LoanDto): Record<string, any> {
    const statusLabel =
        l.status === StatusType.Published ? 'Опубликовано'
            : l.status === StatusType.Unpublished ? 'Снято'
                : String(l.status)

    return {
        ...l,
        amount: formatAmountWithCurrency(l.amount),
        termValue: formatTermDays(l.termValue),
        interestValue: formatInterestPercent(l.interestValue),
        createdAt: formatDateTimeIsoToDDMMYYYY_HHMM(l.createdAt),
        statusLabel
    } as Record<string, any>
}

export function mapLoansToDisplay(loans: LoanDto[] = []) {
    return loans.map(mapLoanToDisplay)
}
