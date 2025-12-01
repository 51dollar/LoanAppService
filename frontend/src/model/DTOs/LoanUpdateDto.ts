import type {StatusType} from '../types/StatusType.ts';

export interface LoanUpdateDto {
    number?: string;
    status?: StatusType
    amount?: number;
    termValue?: number;
    interestValue?: number;
}