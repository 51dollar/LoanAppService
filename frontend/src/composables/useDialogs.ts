import {ref, type ComponentPublicInstance} from 'vue';

export function useDialogs<T extends ComponentPublicInstance>() {
    const dialogRef = ref<T | null>(null);

    const openDialog = () => dialogRef.value?.open();
    const closeDialog = () => dialogRef.value?.close?.();

    return { dialogRef, openDialog, closeDialog };
}
