import {StatusType} from './StatusType.ts';

export interface LoanQuery {
    status?: StatusType;

    amountMin?: number | null;
    amountMax?: number | null;

    termMin?: number | null;
    termMax?: number | null;

    orderBy?: string | null;
    sortDirection?: number | null;
}