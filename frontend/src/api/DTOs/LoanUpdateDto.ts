import type {StatusType} from './StatusType.ts';

export interface LoanUpdateDto {
    number?: string;
    status?: StatusType
    amount?: number;
    termValue?: number;
    interestValue?: number;
}