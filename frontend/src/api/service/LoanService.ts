import {api} from '../axios';
import type {LoanDto} from "../DTOs/LoanDto";
import type {LoanCreateDto} from '../DTOs/LoanCreateDto.ts';
import type {LoanUpdateDto} from '../DTOs/LoanUpdateDto.ts';
import type {PageResult} from '../types/PageResult';

interface TypeResponse {
    data?: LoanDto[];
    totalCount?: number;
}

export async function getLoans(
    pageNumber = 1,
    pageSize = 10
): Promise<PageResult<LoanDto>> {

    const response = await api.get<TypeResponse>('/loans', {
        params: {
            PageNumber: pageNumber,
            PageSize: pageSize
        }
    });

    return {
        data: Array.isArray(response.data.data) ? response.data.data : [],
        totalCount: typeof response.data.totalCount === 'number' ? response.data.totalCount : 0,
    };
}

export async function createLoan(createDto: LoanCreateDto): Promise<LoanDto | null> {
    const response = await api.post<LoanDto>('/loans', createDto);
    return response.data ?? null;
}

export async function updateLoan(
    number: string,
    updatedData: Partial<LoanUpdateDto>
): Promise<boolean> {

    await api.patch(`/loans/${number}`, updatedData);
    return true;
}

export async function deleteLoan(number: string): Promise<boolean> {
    await api.delete(`/loans/${number}`);
    return true;
}
