import { SETREPORT } from "../types";

const initState = {
    count: 0,
    maxDuplicate: 0,
    results: []
}

export const reportReducer = (state = initState, action) => {
    switch (action.type) {
        case SETREPORT:
            return {...state, 
                count: action.payload.count,
                maxDuplicate: action.payload.maxDuplicate,
                results: action.payload.results
            };
        default:
            return state
    }
}

