<script setup lang="ts">
import {ref, reactive} from 'vue';
import type {LoanQuery} from '../../model/types/LoanQuery';

const emit = defineEmits<{
  (e: 'apply', filters: LoanQuery): void
  (e: 'close'): void
}>();

const dialogRef = ref<HTMLDialogElement | null>(null);

const local = reactive<LoanQuery>({
  status: undefined,
  amountMin: null,
  amountMax: null,
  termMin: null,
  termMax: null,
  orderBy: null,
  sortDirection: null
});

function open(initial?: Partial<LoanQuery>) {
  if (initial) Object.assign(local, initial);

  dialogRef.value?.showModal();
}

function close() {
  dialogRef.value?.close();
  emit('close');
}

function reset() {
  local.status = undefined;
  local.amountMin = null;
  local.amountMax = null;
  local.termMin = null;
  local.termMax = null;
  local.orderBy = null;
  local.sortDirection = null;
}

function applyFilters() {
  const payload: LoanQuery = {
    status: local.status,
    amountMin: local.amountMin,
    amountMax: local.amountMax,
    termMin: local.termMin,
    termMax: local.termMax,
    orderBy: local.orderBy,
    sortDirection: local.sortDirection
  };
  emit('apply', payload);
  dialogRef.value?.close();
}

defineExpose({open});
</script>

<template>
  <dialog ref="dialogRef">
    <button class="close-button" @click="close">x</button>

    <div class="header">Фильтры заявок</div>

    <div class="content">
      <div class="wrapper">
      <label class="label">Статус</label>
      <select v-model="local.status" class="input">
        <option :value="undefined">Все</option>
        <option :value="1">Опубликованный</option>
        <option :value="2">Снятый с публикации</option>
      </select>
      </div>

      <div class="wrapper">
      <label class="label">Сортировать по</label>
      <select v-model="local.sortDirection" class="input">
        <option :value="null">Не отсортировывать</option>
        <option :value="1">По возрастанию</option>
        <option :value="2">По убыванию</option>
      </select>
      <label class="label">Выбери колонку</label>
        <select v-model="local.orderBy" class="input">
          <option :value="null">Не выбрано</option>
          <option value="Number">Номер заявки</option>
          <option value="Amount">Сумма</option>
          <option value="TermValue">Срок</option>
          <option value="InterestValue">Процент</option>
          <option value="CreatedAt">Дата создания</option>
          <option value="Status">Статус</option>
        </select>
      </div>

      <div class="wrapper">
      <label class="label">Сумма от</label>
      <input type="number" class="input" v-model.number="local.amountMin" placeholder="Введите минимальную сумму" />

      <label class="label">Сумма до</label>
      <input type="number" class="input" v-model.number="local.amountMax" placeholder="Введите максимальную сумму" />

      <label class="label">Срок от (в днях)</label>
      <input type="number" class="input" v-model.number="local.termMin" placeholder="Введите минимальное количество дней" />

      <label class="label">Срок до (в днях)</label>
      <input type="number" class="input" v-model.number="local.termMax" placeholder="Введите максимальное количество дней" />
      </div>
    </div>

    <div class="actions">
      <button class="default-button" @click="reset">Сбросить</button>
      <button class="create-button" @click="applyFilters">Применить</button>
    </div>
  </dialog>
</template>

<style scoped>
dialog {
  border: none;
  padding: 20px;
  border-radius: 24px;

  background: rgba(30, 30, 30, 0.75);
  backdrop-filter: blur(16px);
  -webkit-backdrop-filter: blur(16px);

  width: min(90vw, 460px);
  max-width: 460px;

  position: fixed;
  inset: 0;
  margin: auto;

  color: #fff;
  animation: dialogIn 0.25s ease;
}

dialog::backdrop {
  background: rgba(0, 0, 0, 0.55);
  backdrop-filter: blur(6px);
}

.header {
  padding-bottom: 10px;
  font-size: 1.5rem;
  font-weight: bold;
  margin-top: 0.5rem;
  text-align: center;
}

.label {
  padding: 8px 0 4px 5px;
  display: block;
  font-size: 1.125rem;
}

.content {
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.input {
  width: 100%;
  border-radius: 24px;

  background: rgba(255, 255, 255, 0.08);
  backdrop-filter: blur(12px);
  -webkit-backdrop-filter: blur(12px);

  color: #fff;
  padding-left: 15px;
  height: 36px;
  border: 1px solid transparent;
  outline: none;

  transition: 0.2s ease;
}

.input:focus {
  border-color: rgba(255, 255, 255, 0.25);
}

select.input option {
  background: rgba(30, 30, 30, 0.75);
}

.actions {
  display: flex;
  justify-content: center;
  gap: 10px;
  margin-top: 10px;
}

.wrapper {
  width: 100%;
  padding: 10px;
  margin: 4px;

  background: rgba(255, 255, 255, 0.03);
  border-radius: 24px;
  overflow: hidden;
  overflow-x: auto;

  box-shadow:
      0 0 0 1px rgba(255, 255, 255, 0.04),
      0 2px 5px rgba(0, 0, 0, 0.4);

  backdrop-filter: blur(6px);
  -webkit-backdrop-filter: blur(6px);
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