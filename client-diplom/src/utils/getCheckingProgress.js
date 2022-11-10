import axios from "axios"

export const getProgressChecking = (progressName, token, setProgressComplete) => {
    return setInterval(async () => {
        await axios.get(`http://localhost:5229/Progress/${progressName}`,{
            headers:{
                "Authorization": "Bearer " + token,
            }
        })
        .then(res => {
            const data = res.data
            setProgressComplete((data.progress / data.allItems) * 100)
            if(data.progress === data.allItems){
                clearInterval(this)
                return
            }
        })
    }, 500)
}