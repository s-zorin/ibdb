import type CommonResultDto from '@/dtos/commonResultDto'
import type BookDto from '@/dtos/bookDto'
import type ReviewDto from '@/dtos/reviewDto'
import type PageDto from '@/dtos/pageDto'
import Notifications from '@/services/notifications'

interface QueryKeyValue {
    key: string
    value: any
}

module Api {
    async function get(path: string, parameters?: QueryKeyValue[]): Promise<Response> {
        const url = createUrl(path, parameters)

        const get: RequestInit = {
            method: 'GET',
            headers: {
                'content-type': 'application/json;charset=UTF-8',
            }
        }
    
        return await fetch(url, get)
    }

    async function post(path: string, parameters: QueryKeyValue[] | undefined, body: object | undefined): Promise<Response> {
        const url = createUrl(path, parameters)

        const post: RequestInit = {
            method: 'POST',
            headers: {
                'content-type': 'application/json;charset=UTF-8',
            },
            body: JSON.stringify(body)
        }

        return await fetch(url, post)
    }

    async function put(path: string, parameters: QueryKeyValue[] | undefined, body: object | undefined): Promise<Response> {
        const url = createUrl(path, parameters)

        const put: RequestInit = {
            method: 'PUT',
            headers: {
                'content-type': 'application/json;charset=UTF-8',
            },
            body: JSON.stringify(body)
        }

        return await fetch(url, put)
    }

    // TIL : You can't name a method 'delete' in typescipt.
    async function _delete(path: string, parameters: QueryKeyValue[] | undefined, body: object | undefined): Promise<Response> {
        const url = createUrl(path, parameters)

        const put: RequestInit = {
            method: 'DELETE',
            headers: {
                'content-type': 'application/json;charset=UTF-8',
            },
            body: JSON.stringify(body)
        }

        return await fetch(url, put)
    }

    function createUrl(path: string, parameters: QueryKeyValue[] | undefined): string {
        return parameters != undefined && parameters.length > 0
            ? `http://${location.hostname}/${path}?${parameters?.map(p => `${p.key}=${p.value}`).join('&')}`
            : `http://${location.hostname}/${path}`
    }

    export async function getBooks(skip: number, take: number): Promise<CommonResultDto<PageDto<BookDto[]>>> {
        const response = await get(
            'api/books',
            [
                { key: 'skip', value: skip },
                { key: 'take', value: take }
            ])

        return (await response.json()) as CommonResultDto<PageDto<BookDto[]>>
    }

    export async function getBook(id: string): Promise<CommonResultDto<BookDto>> {
        const response = await get(`api/books/${id}`)

        return (await response.json()) as CommonResultDto<BookDto>
    }

    export async function createBook(id: string, title: string, description: string | undefined, callback: (result: CommonResultDto<string>) => void) {
        let operationId = await Notifications.register(callback)

        post(
            'api/books',
            [
                { key: 'operationId', value: operationId }
            ],
            {
                id: id,
                title: title,
                description: description
            }
        )
    }

    export async function updateBook(id: string, title: string, description: string | undefined, callback: (result: CommonResultDto<string>) => void) {
        let operationId = await Notifications.register(callback)

        put(
            'api/books',
            [
                { key: 'operationId', value: operationId }
            ],
            {
                id: id,
                title: title,
                description: description
            }
        )
    }

    export async function deleteBook(id: string, callback: (result: CommonResultDto<string>) => void) {
        let operationId = await Notifications.register(callback)

        _delete(
            'api/books',
            [
                { key: 'operationId', value: operationId }
            ],
            {
                id: id
            }
        )
    }

    export async function getReviews(skip: number, take: number): Promise<CommonResultDto<PageDto<ReviewDto[]>>> {
        const response = await get(
            'api/reviews',
            [
                { key: 'skip', value: skip },
                { key: 'take', value: take }
            ])

        return (await response.json()) as CommonResultDto<PageDto<ReviewDto[]>>
    }

    export async function getReview(id: string): Promise<CommonResultDto<ReviewDto>> {
        const response = await get(`api/reviews/${id}`)

        return (await response.json()) as CommonResultDto<ReviewDto>
    }

    export async function createReview(id: string, bookId: string, text: string | undefined, score: number, callback: (result: CommonResultDto<string>) => void) {
        let operationId = await Notifications.register(callback)

        post(
            'api/reviews',
            [
                { key: 'operationId', value: operationId }
            ],
            {
                id: id,
                bookId: bookId,
                text: text,
                score: score
            }
        )
    }

    export async function updateReview(id: string, text: string | undefined, score: number, callback: (result: CommonResultDto<string>) => void) {
        let operationId = await Notifications.register(callback)

        put(
            'api/reviews',
            [
                { key: 'operationId', value: operationId }
            ],
            {
                id: id,
                text: text,
                score: score
            }
        )
    }

    export async function deleteReview(id: string, callback: (result: CommonResultDto<string>) => void) {
        let operationId = await Notifications.register(callback)

        _delete(
            'api/reviews',
            [
                { key: 'operationId', value: operationId }
            ],
            {
                id: id
            }
        )
    }
}

export default Api
