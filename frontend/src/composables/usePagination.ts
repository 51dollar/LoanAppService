import { ref } from 'vue';

export function usePagination(page = 1, size = 10) {
    const pageNumber = ref(page);
    const pageSize = ref(size);

    const goToPage = (page: number, callback: (page: number) => void) => {
        if (page < 1) return;
        pageNumber.value = page;
        callback(page);
    };

    const nextPage = (callback: (page: number) => void) => goToPage(pageNumber.value + 1, callback);
    const prevPage = (callback: (page: number) => void) => goToPage(pageNumber.value - 1, callback);

    return { pageNumber, pageSize, goToPage, nextPage, prevPage };
}
