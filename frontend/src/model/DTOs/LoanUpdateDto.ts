import type {StatusType} from '../types';

export interface LoanUpdateDto {
    number?: string;
    status: StatusType
    amount?: number;
    termValue?: number;
    interestValue?: number;
}