import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCommentBySlugOfProduct } from "../../../api/Comment";
import "../../../styles/user/ProductDetail.css";
import { format } from "date-fns";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser } from "@fortawesome/free-regular-svg-icons";

const ProductComment = () => {
  const [comment, setComment] = useState([]);
  const params = useParams();
  const { slug } = params;

  useEffect(() => {
    getCommentBySlugOfProduct(slug).then((data) => {
      if (data) {
        setComment(data.items);
      } else {
        setComment({});
      }
    });
  }, [slug]);

  return (
    <section className="comment_container">
      <div className="comment_content">
        <h5 className="comment_title">Bình luận đã để lại</h5>
      </div>
      <div className="comment_wrapper">
        {comment.length > 0 ? (
          comment.map((item, index) => (
            <div className="comment_item row " key={index}>
              <div className="comment_item_col col-2">
                <FontAwesomeIcon icon={faUser} fontSize={60} />
              </div>
              <div className="comment_item_col col-10">
                <div className="comment_item_col_top row">
                  <h5 className="col">Comment{item.name}</h5>
                  <div className="col">
                    Ngày đăng {format(new Date(item.created), "dd/MM/yyyy")}
                  </div>
                </div>
                <div className="comment_item_col_bottom">
                  Content {item.commentText}
                </div>
              </div>
            </div>
          ))
        ) : (
          <>Khong có comment</>
        )}
      </div>
    </section>
  );
};
export default ProductComment;
