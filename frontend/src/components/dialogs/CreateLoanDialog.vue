<script setup lang="ts">
import {ref} from 'vue';
import type {LoanCreateDto} from '../../validators/loanSchemas';
import {validateLoanCreate, type LoanErrors} from '../../validators/loanValidator';
import {X} from 'lucide-vue-next';

const emit = defineEmits<{
  (e: 'result', value: LoanCreateDto | null): void
}>();

const dialog = ref<HTMLDialogElement | null>(null);

const form = ref<LoanCreateDto>({
  number: '',
  amount: 0,
  termValue: 0,
  interestValue: 0
});

const errors = ref<LoanErrors>({
  number: '',
  amount: '',
  termValue: '',
  interestValue: ''
});

const validate = () => {
  const validationErrors = validateLoanCreate(form.value);

  errors.value = {
    number: validationErrors.number || '',
    amount: validationErrors.amount || '',
    termValue: validationErrors.termValue || '',
    interestValue: validationErrors.interestValue || ''
  };

  return Object.keys(validationErrors).length === 0;
};

const open = () => {
  form.value = {
    number: '',
    amount: 0,
    termValue: 0,
    interestValue: 0
  };
  errors.value = {
    number: '',
    amount: '',
    termValue: '',
    interestValue: ''
  };

  dialog.value?.showModal();
};

const close = () => {
  dialog.value?.close();
  emit('result', null);
};

const submit = () => {
  if (!validate()) {
    console.log('Ошибка валидации', errors);
    return;
  }

  emit('result', form.value);
  dialog.value?.close();
};

defineExpose({open});
</script>

<template>
  <dialog ref="dialog">
    <button class="close-button" @click="close">
      <X />
    </button>

    <h3 class="header">Создание заявки</h3>

    <div class="space-y-4 w-full">
      <div>
        <label class="label">Номер займа</label>
        <input
            v-model="form.number"
            type="text"
            class="input w-full"
            placeholder="Введите номер"
        />
        <p v-if="errors.number" class="text-error">{{ errors.number }}</p>
      </div>

      <div>
        <label class="label">Сумма займа</label>
        <input
            v-model.number="form.amount"
            type="number"
            min="0"
            class="input"
            placeholder="Введите сумму"
        />
        <p v-if="errors.amount" class="text-error">{{ errors.amount }}</p>
      </div>

      <div>
        <label class="label">Срок займа</label>
        <input
            v-model.number="form.termValue"
            type="number"
            min="0"
            class="input"
            placeholder="Введите срок в днях"
        />
        <p v-if="errors.termValue" class="text-error">{{ errors.termValue }}</p>
      </div>

      <div>
        <label class="label">Процентная ставка</label>
        <input
            v-model.number="form.interestValue"
            type="number"
            min="0"
            step="0.1"
            class="input"
            placeholder="Введите процент"
        />
        <p v-if="errors.interestValue" class="text-error">{{ errors.interestValue }}</p>
      </div>
    </div>

    <div class="actions">
      <button
          @click="submit"
          class="create-button"
      >
        Создать
      </button>
    </div>
  </dialog>
</template>

<style scoped>
dialog {
  border: none;
  padding: 10px;
  border-radius: 24px;

  background: rgba(30, 30, 30, 0.75);
  backdrop-filter: blur(16px);
  -webkit-backdrop-filter: blur(16px);

  width: min(90vw, 420px);
  max-width: 420px;

  color: #fff;

  position: fixed;
  inset: 0;
  margin: auto;

  animation: dialogIn 0.25s ease;
}

dialog::backdrop {
  background: rgba(0, 0, 0, 0.55);
  backdrop-filter: blur(6px);
}

.header {
  padding: 0 0 10px 0;
  font-size: 1.5rem;
  font-weight: bold;
  margin-top: 0.5rem;
  text-align: center;
}

.label {
  padding: 0 0 4px 20px;
  display: block;
  font-size: 1.125rem;
}

.actions {
  display: flex;
  justify-content: center;
  gap: 0.75rem;
  margin-top: 1.5rem;
}

.text-error {
  color: #ef4444;
  font-size: 0.875rem;
}

input {
  width: 100%;
  border-radius: 24px;

  background: rgba(255, 255, 255, 0.08);
  backdrop-filter: blur(12px);
  -webkit-backdrop-filter: blur(12px);

  text-align: left;
  padding-left: 15px;
  color: #fff;
  outline: none;
  transition: all 0.2s ease;

  height: 30px;
}

@keyframes dialogIn {
  from {
    opacity: 0;
    transform: scale(0.92) translateY(10px);
  }
  to {
    opacity: 1;
    transform: scale(1) translateY(0);
  }
}
</style>
