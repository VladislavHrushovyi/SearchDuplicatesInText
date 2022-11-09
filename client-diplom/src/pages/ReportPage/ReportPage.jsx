import { Col, List, Pagination, Typography } from "antd"
import React from "react"
import { useState } from "react"
import { useSelector } from "react-redux"

export const ReportPage = () => {
    const [itemsRange, setItemsRange] = useState({left:0, right:10})
    const reportSelector = useSelector(s => s.report)
    const pageSize = 10
    
    const onPageChange = (e) => {
        if(e === 1){
            setItemsRange({
                ...itemsRange,
                left: 0,
                right: pageSize 
            })    
        }
        setItemsRange({
            ...itemsRange,
            left: (e-1) * pageSize,
            right: (e-1) * pageSize + pageSize
        })
    }

    return (
        <>
        <div className="site-layout-content">
            <Col gutter={{ xs: 8, sm: 16, md: 24, lg: 32 }} style={{ marginTop: "25px" }}>
                    
                        <List
                            header={<div>Header</div>}
                            footer={<Pagination
                                total={reportSelector.results.length}
                                showTotal={(total, range) => `${range[0]}-${range[1]} of ${total} items`}
                                defaultPageSize={pageSize}
                                defaultCurrent={1}
                                onChange={onPageChange}
                            />}
                            bordered
                            dataSource={reportSelector.results.slice(itemsRange.left,itemsRange.right)}
                            renderItem={item => (
                                <List.Item >
                                    <Typography.Text mark >[ФАЙЛ]</Typography.Text> {item.nameFile}

                                    <Typography.Text style={{marginLeft:'25px'}} mark>[ЗХОЖІСТЬ]</Typography.Text> <Typography.Text  type={parseInt(item.percent) < 25 ?
                                        "success" : parseInt(item.percent) > 25 && parseInt(item.percent) < 50
                                            ? "warning"
                                            : "danger"} >{parseInt(item.percent)}%</Typography.Text>
                                </List.Item>
                            )}
                        />
            </Col>
            </div>
        </>
    )
} 