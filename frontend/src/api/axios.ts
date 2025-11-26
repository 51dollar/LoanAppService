import axios from 'axios';

export const api = axios.create({
    baseURL: import.meta.env.VITE_API_BASE || '/api'
});

function handleApiResponse(response: any) {
    const status = response.status;

    switch (status) {
        case 200:
            console.log('Запрос успешен:', response.data);
            break;
        case 201:
            console.log('Ресурс создан:', response.data);
            break;
        case 204:
            console.log('Запрос выполнен, данные отсутствуют');
            break;
        default:
            console.warn(`Необычный статус ответа (${status}):`, response.data);
    }

    return response;
}

function handleApiError(error: any) {
    if (error.response) {
        const status = error.response.status;
        const message = error.response.data?.message || error.message;

        switch (status) {
            case 400:
                console.error('Некорректный запрос:', message);
                break;
            case 404:
                console.error('Ресурс не найден');
                break;
            case 500:
                console.error('Ошибка сервера');
                break;
            default:
                console.error(`Ошибка API (${status}):`, message);
        }
    } else if (error.request) {
        console.error('Сервер не отвечает');
    } else {
        console.error('Ошибка запроса:', error.message);
    }

    return Promise.reject(error);
}

api.interceptors.response.use(
    response => handleApiResponse(response),
    error => handleApiError(error)
);