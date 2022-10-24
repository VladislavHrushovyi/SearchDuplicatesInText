import './App.css';
import 'antd/dist/antd.min.css';
import { Header } from './components/Header/Header';
import { Layout, Space } from 'antd';
import { Content } from 'antd/lib/layout/layout';
import { Routes, Route } from "react-router-dom";
import { MainPage } from './pages/MainPage/MainPage';
import { ReportPage } from './pages/ReportPage/ReportPage';
import { Provider } from 'react-redux';
import  store  from "./redux/store"

function App() {
  return (
    <>
    <Provider store={store}>
    <Layout className='layout'>
      <Header />
      <Content style={{ padding: '0 50px' }}>
        <Routes>
          <Route path='/' element={<MainPage />} />
          <Route path='main' element={<MainPage />}/>
          <Route path='report' element={<ReportPage />}/>
        </Routes>
      </Content>
    </Layout>
    </Provider>
    </>
  );
}

export default App;
