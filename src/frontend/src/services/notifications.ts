import { v4 as uuid } from 'uuid';
import * as signalR from "@microsoft/signalr";
import type CommonResultDto from '@/dtos/commonResultDto';
import type ErrorDto from '@/dtos/errorDto';

class OperationCallbackPair {
    operationId: string
    callback: (result: CommonResultDto<string>) => void

    constructor(operationId: string, callback: (result: CommonResultDto<string>) => void) {
        this.operationId = operationId   
        this.callback = callback
    }
}

module Notifications {
    let connection: signalR.HubConnection
    let operationCallbackPairs: OperationCallbackPair[] = []

    async function getConnection(): Promise<signalR.HubConnection> {
        if (connection != undefined)
            return connection

        connection = new signalR.HubConnectionBuilder()
            .withUrl(`http://${location.hostname}/api/notifications`)
            .build()

        await connection.start().catch((error) => console.log(error));

        connection.on('Completed', (result: CommonResultDto<string>) => {
            let pair = operationCallbackPairs.find((pair) => pair.operationId == result.value)

            if (pair == undefined)
                return

            pair.callback(result)

            operationCallbackPairs.splice(operationCallbackPairs.indexOf(pair), 1)
        })

        connection.on

        return connection
    }

    export async function register(callback: (result: CommonResultDto<string>) => void): Promise<string> {
        let operationId = uuid()
        let connection = await getConnection()

        operationCallbackPairs.push(new OperationCallbackPair(operationId, callback))

        await connection.send('Register', operationId)

        return operationId
    }
}

export default Notifications