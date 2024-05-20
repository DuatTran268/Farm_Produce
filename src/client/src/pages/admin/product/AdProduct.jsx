import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { Link, useParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAdd, faEdit, faEye, faEyeSlash, faTrash } from "@fortawesome/free-solid-svg-icons";
import { Table } from "react-bootstrap";
import HeaderBtn from "../../../components/common/HeaderBtn";
import BtnSuccess from "../../../components/common/BtnSuccess";
import BtnNextPage from "../../../components/common/BtnNextPage";
import Loading from "../../../components/common/Loading";
import { useSnackbar } from "notistack";
import { useSelector } from "react-redux";
import { deletProduct, getFilterProduct } from "../../../api/Product";
import ProductFilter from "../../../components/admin/filter/ProductFilter";
import Popup from "../../../components/common/Popup";
import "./AdProduct.css"
const AdProduct = () => {
  const [getProduct, setgetProduct] = useState([]);
  const [refreshData, setRefreshData] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [metadata, setMetadata] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  const [isVisibleLoading, setIsVisibleLoading] = useState(true),
    productFilter = useSelector((state) => state.productFilter);


  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const [IdToDelete, setIdToDelete] = useState(null);
 // Thêm useState để lưu trạng thái sắp xếp
 const [sortOrder, setSortOrder] = useState();
 const [sortColumn, setSortColumn] = useState("Price");

 const handleSortChange = (selectedSortOrder) => {
   // Cập nhật giá trị của sortOrder
   setSortOrder(selectedSortOrder);
 };

  let { id } = useParams,
    p = 1,
    ps = 5;
  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    document.title = "Quản lý Sản phẩm";

    loadDepartment();
    async function loadDepartment() {
      function setData(props) {
        setgetProduct(props.items);
        setMetadata(props.metadata);
      }
      getFilterProduct(productFilter.name, "", ps, pageNumber, sortColumn, sortOrder).then((data) => {
        if (data) {
          console.log("Check data of manage product: ", data)
          setData(data);
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }
  }, [productFilter, ps, p, refreshData, pageNumber, sortColumn, sortOrder]);


  
  const handleDelete = (id) => {
    setIdToDelete(id);
    setPopupMessage("Bạn có muốn xoá sản phẩm này?");
    setPopupVisible(true);
  };

  const handleConfirmDelete = async () => {
    const response = await deletProduct(IdToDelete);
    if (response) {
      enqueueSnackbar("Đã xoá thành công", {
        variant: "success",
      });
      setRefreshData((prev) => !prev);
    } else {
      enqueueSnackbar("Xoá thất bại", {
        variant: "error",
      });
    }
    setPopupVisible(false);
  };

  const handleCancelDelete = () => {
    setPopupVisible(false);
  };

   // Hàm định dạng giá tiền thành VNĐ
   const formatCurrency = (number) => {
    return number.toLocaleString("vi-VN", {
      style: "currency",
      currency: "VND",
    });
  };

  return (
    <>
      <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý Sản phẩm</h3>
      </div>

      <HeaderBtn>
        <BtnSuccess icon={faAdd} slug={"/admin/product/edit"} name="Thêm mới" />

          
        <ProductFilter/>
        <select onChange={(e) => handleSortChange(e.target.value)} className="option_sort">
          <option >---Sắp xếp theo giá---</option>
            <option value="ASC">Giá tăng dần</option>
            <option value="DESC">Giá giảm dần</option>
          </select>
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>Tên Sản phẩm</th>
                <th>Lượt xem</th>
                <th>Tồn kho</th>
                <th>Giá</th>
                <th>Mô tả</th>
                <th>Trạng thái</th>
                <th>Sửa</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getProduct.length > 0 ? (
                getProduct.map((item, index) => (
                  <tr key={index}>
                    <td>{item.name}</td>
                    <td>{item.viewCount} <FontAwesomeIcon icon={faEye} color="red"/></td>
                    <td>{item.quantityAvailable}</td>
                    <td>{formatCurrency(item.price)} / {item.unit.name}</td>
                    <td>
                      <div className="table_description">
                      {item.description}
                      </div>
                      </td>
                    <td>
                    {item.status ? (
                            <span className="text-success">Hiển thị <FontAwesomeIcon icon={faEye}/> </span>
                          ) : (
                            <span className="text-danger">Đã Ẩn <FontAwesomeIcon icon={faEyeSlash}/></span>
                          )}
                    </td>
                    <td className="text-center">
                      <Link
                        to={`/admin/product/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faEdit} />
                      </Link>
                    </td>
                    <td className="text-center">
                      <div onClick={() => handleDelete(item.id)}>
                        <FontAwesomeIcon icon={faTrash} color="red" />
                      </div>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan={8}>
                    <h4 className="text-danger text-center">Không tìm thấy</h4>
                  </td>
                </tr>
              )}
            </tbody>
          </Table>
        )}
        <BtnNextPage metadata={metadata} onPageChange={updatePageNumber} />
      </div>

      {popupVisible && (
        <Popup
          message={popupMessage}
          onCancel={handleCancelDelete}
          onConfirm={handleConfirmDelete}
        />
      )}


      </LayoutCommon>
    </>
  )
  
};
export default AdProduct;
