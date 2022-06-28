import type CommonResultDto from '../dtos/commonResultDto'
import type BookDto from '../dtos/bookDto'

interface QueryKeyValue {
    key: string
    value: any
}

module Api {
    async function get(path: string, parameters: QueryKeyValue[] | undefined): Promise<Response> {
        const get = {
            method: 'GET', headers: {
                'content-type': 'application/json;charset=UTF-8',
            }
        }

        const url = parameters != undefined && parameters.length > 0
            ? `http://${location.hostname}/${path}?${parameters?.map(p => `${p.key}=${p.value}`).join('&')}`
            : `http://${location.hostname}/${path}`
    
        return await fetch(url, get)
    }

    export async function getBooks(skip: number, take: number): Promise<CommonResultDto<BookDto[]>> {
        const response = await get(
            'api/books',
            [
                { key: 'skip', value: skip },
                { key: 'take', value: take }
            ])
        return (await response.json()) as CommonResultDto<BookDto[]>
    }
}

export default Api
