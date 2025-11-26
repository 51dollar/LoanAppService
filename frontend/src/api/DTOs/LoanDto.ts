import {StatusType} from './StatusType.ts';

export interface LoanDto {
    status: StatusType;
    number: string;
    amount: number;
    termValue: number;
    interestValue: number;
    createdAt: string;
}