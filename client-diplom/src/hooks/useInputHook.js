import { useState } from "react"

export const useInputHook = (init, name) => {
    const [value, setValue] = useState(init)

    return {
        name: name,
        value: value,
        onChange: (e) => {
            setValue(e.target.value)
        }
    }
}