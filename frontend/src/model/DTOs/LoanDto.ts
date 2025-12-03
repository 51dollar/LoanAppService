import {StatusType} from '../types';

export interface LoanDto {
    status: StatusType;
    number: string;
    amount: number;
    termValue: number;
    interestValue: number;
    createdAt: string;
}