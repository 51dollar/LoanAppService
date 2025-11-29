import { LoanCreateSchema, type LoanCreateDto } from './loanSchemas';

export type LoanErrors = Partial<Record<keyof LoanCreateDto, string>>;

export function validateLoanCreate(form: LoanCreateDto): LoanErrors {
    const result = LoanCreateSchema.safeParse(form);

    if (result.success) {
        return {};
    }

    const errors: LoanErrors = {};

    result.error.issues.forEach(issue => {
        const field = issue.path[0] as keyof LoanCreateDto;
        errors[field] = issue.message;
    });

    return errors;
}