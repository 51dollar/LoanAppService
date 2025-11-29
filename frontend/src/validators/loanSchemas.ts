import {z} from 'zod';

export const LoanCreateSchema = z.object({
    number: z.string()
        .min(1, 'Номер заявки обязателен')
        .max(50, 'Номер заявки не должен превышать 50 символов'),
    amount: z.number()
        .positive('Сумма должна быть больше 0')
        .max(100000000, 'Сумма слишком большая'),
    termValue: z.number()
        .positive('Срок должен быть больше 0')
        .int('Срок должен быть целым числом')
        .max(3650, 'Срок не может превышать 10 лет'),
    interestValue: z.number()
        .positive('Процент должен быть больше 0')
        .max(1000, 'Процентная ставка слишком высока')
});

export type LoanCreateDto = z.infer<typeof LoanCreateSchema>;