export interface ReviewModel {
  averageRate: number
  authorName: string
  authorLikesCount: number
  likesCount: number
  reviewTitle: string
  productName: string
  category: string
  authorRate: number
  tags: string[]
  imageUrl: string
  creationDate: Date
  isLike: boolean
  userRating: number
  description: string
}
