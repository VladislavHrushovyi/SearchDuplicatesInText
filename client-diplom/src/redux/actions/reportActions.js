import { SETREPORT } from "../types"

export const setReport = (data) => {
    return {
        type: SETREPORT,
        payload: data
    }
}