import { Button, Col, Form, Input, Row } from "antd";
import axios from "axios";
import React from "react";
import { openNotificationWithIcon } from '../../utils/notification';

export const SignUpForm = () => {

    const onFinish = (values) => {
        axios.post("https://localhost:7263/auth/register", values)
                .then(res => {
                    console.log(res)
                    openNotificationWithIcon('success', { message: "Реєстрація пройшла успішно", description:`Користувач ${res.data.nickname}. Перейдіть до авторизації` })
                })
                .catch(function(error){
                    openNotificationWithIcon('error', { title: "Упс", description:`Щось пішло не так` })
                    console.log(error)
                });
    };
    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);
    };

    return (
        <>
            <Row justify="center">
                <Col span={24}>
                    <Form
                        name="basic"
                        labelCol={{
                            span: 6,
                        }}
                        wrapperCol={{
                            span: 12,
                        }}
                        initialValues={{
                            remember: true,
                        }}
                        onFinish={onFinish}
                        onFinishFailed={onFinishFailed}
                        autoComplete="off">
                        <Form.Item label="Ім'я користувача"
                            name="Nickname"
                            rules={[
                                {   
                                    min:5,
                                    message:"Мінімум 5 символів",
                                },
                                {
                                    required: true,
                                    message: "Поле не може бути пустим!",
                                },
                            ]}>
                            <Input placeholder="Введіть ім'я користувача"/>
                        </Form.Item>
                        <Form.Item label="Електронна пошта"
                            name="Email"
                            rules={[
                                {
                                    type:"email",
                                    message:"Невірний формат"
                                },
                                {
                                    required: true,
                                    message: "Поле не може бути пустим!",
                                },
                            ]}>
                            <Input  placeholder="Введіть електронну пошту"/>
                        </Form.Item>
                        <Form.Item
                            label="Пароль"
                            name="Password"
                            rules={[
                                {
                                    min:6,
                                    message: "Мінімум 6 символів"
                                },
                                {
                                    required: true,
                                    message: 'Поле не може бути пустим!',
                                },
                            ]}
                        >
                            <Input.Password placeholder="Введіть пароль"/>
                        </Form.Item>
                        <Form.Item
                            label="Підтвердження пароля"
                            name="confirm"
                            dependencies={['password']}
                            rules={[
                                {
                                    min: 6,
                                    message: "Мінімум 6 символів"
                                },
                                {
                                    required: true,
                                    message: 'Введіть повторно пароль! Мінімум 6 символів',
                                },
                                ({ getFieldValue }) => ({
                                    validator(_, value){
                                        if(!value || getFieldValue('Password') === value){
                                            return Promise.resolve()
                                        }
                                        return Promise.reject(new Error('Паролі не співпадають'));
                                    }
                                })
                            ]}
                        >
                            <Input.Password placeholder="Введіть повторно пароль"/>
                        </Form.Item>
                        <Form.Item
                            wrapperCol={{
                                offset: 8,
                                span: 16,
                            }}
                        >
                            <Button type="primary" htmlType="submit" size="large">
                                Зареєструватися
                            </Button>
                        </Form.Item>
                    </Form>
                </Col>
            </Row>
        </>
    )
}