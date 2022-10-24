import React from "react"
import { useSelector } from "react-redux"

export const ReportPage = () => {
    const reportSelector = useSelector(s => s.report)

    return (
        <>
        <div className="site-layout-content">
            {reportSelector.results.map((r,index) => 
                <p key={index}>{r.nameFile}--{r.percent}</p>
            )}
            </div>
        </>
    )
} 