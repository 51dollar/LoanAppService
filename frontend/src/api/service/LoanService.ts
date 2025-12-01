import {api} from '../axios';
import type {LoanDto} from "../../model/DTOs/LoanDto";
import type {LoanCreateDto} from '../../model/DTOs/LoanCreateDto.ts';
import type {LoanUpdateDto} from '../../model/DTOs/LoanUpdateDto.ts';
import type {PageResult} from '../../model/types/PageResult';
import type {LoanQuery} from '../../model/types/LoanQuery.ts';
import {StatusType} from '../../model/types/StatusType.ts';

interface TypeResponse {
    data?: LoanDto[];
    totalCount?: number;
}

export async function getLoans(
    filters?: LoanQuery,
    pageNumber = 1,
    pageSize = 10
): Promise<PageResult<LoanDto>> {

    console.log('Входные параметры фильтрации API: ', filters);

    const params: Record<string, any> = {
        PageNumber: pageNumber,
        PageSize: pageSize,
        OrderBy: filters?.orderBy ?? '',
        SortDirection: typeof filters?.sortDirection === 'number' ? filters!.sortDirection : 1
    };


    if (filters?.status !== undefined) {
        switch (filters.status) {
            case 1:
                params.Status = StatusType.Published
                break
            case 2:
                params.Status = StatusType.Unpublished
                break
            default:
                break
        }
    }

    if (filters?.amountMin != null && !Number.isNaN(filters.amountMin)) {
        params.MinAmount = filters.amountMin;
    }
    if (filters?.amountMax != null && !Number.isNaN(filters.amountMax)) {
        params.MaxAmount = filters.amountMax;
    }

    if (filters?.termMin != null && !Number.isNaN(filters.termMin)) {
        params.MinTerm = filters.termMin;
    }
    if (filters?.termMax != null && !Number.isNaN(filters.termMax)) {
        params.MaxTerm = filters.termMax;
    }

    const response = await api.get<TypeResponse>('/loans', {params});

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
