import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { Link, useParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAdd, faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";
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

const AdProduct = () => {
  const [getProduct, setgetProduct] = useState([]);
  const [reRender, setRender] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [metadata, setMetadata] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  const [isVisibleLoading, setIsVisibleLoading] = useState(true),
    productFilter = useSelector((state) => state.productFilter);


  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const [IdToDelete, setIdToDelete] = useState(null);

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
      getFilterProduct(productFilter.name, ps, pageNumber).then((data) => {
        if (data) {
          setData(data);
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }
  }, [productFilter, ps, p, reRender, pageNumber]);


  
  const handleDelete = (id) => {
    setIdToDelete(id);
    setPopupMessage("Bạn có muốn xoá category này?");
    setPopupVisible(true);
  };

  const handleConfirmDelete = async () => {
    const response = await deletProduct(IdToDelete);
    if (response) {
      enqueueSnackbar("Đã xoá thành công", {
        variant: "success",
      });
      setRender(true);
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
  return (
    <>
      <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý Sản phẩm</h3>
      </div>

      <HeaderBtn>
        <BtnSuccess icon={faAdd} slug={"/admin/product/edit"} name="Thêm mới" />
        <ProductFilter/>
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>Tên Sản phẩm</th>
                <th>Số lượng</th>
                <th>Giá</th>
                <th>Mô tả</th>
                <th>Sửa</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getProduct.length > 0 ? (
                getProduct.map((item, index) => (
                  <tr key={index}>
                    <td>{item.name}</td>
                    <td>{item.quanlityAvailable}</td>
                    <td>{item.price}</td>
                    <td>{item.description}</td>
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
                  <td colSpan={3}>
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
