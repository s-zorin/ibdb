import type ErrorDto from './errorDto'

export default class CommonResultDto<T> {
    value: T | undefined
    errors: ErrorDto[] = []
}